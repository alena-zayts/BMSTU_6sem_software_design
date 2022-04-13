﻿using Xunit;
using BL;
using Ninject;
using BL.Models;
using System.Threading.Tasks;
using BL.Exceptions;
using System.Collections.Generic;


namespace TestsBL
{
    public class TestLiftsAndSlopes
    {
        [Fact]
        public async Task Test1()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepositoriesFactory>().To<IoCRepositoriesFactory>();
            Facade facade = new(ninjectKernel.Get<IRepositoriesFactory>());

            await TestUsersCreator.Create();


            Assert.Empty(await facade.GetLiftsInfoAsync(TestUsersCreator.skiPatrolID));


            Lift added_lift1 = new Lift(100000, "A1", true, 100, 60, 360);
            added_lift1 = await facade.AdminAddAutoIncrementLiftAsync(TestUsersCreator.adminID, added_lift1);
            Lift added_lift2 = new Lift(200000, "A2", false, 20, 10, 30);
            await facade.AdminAddLiftAsync(TestUsersCreator.adminID, added_lift2);
            added_lift2 = new Lift(added_lift2.LiftID, added_lift2.LiftName, !added_lift2.IsOpen, added_lift2.SeatsAmount + 1, added_lift2.LiftingTime, added_lift2.QueueTime);
            await facade.UpdateLiftInfoAsync(TestUsersCreator.adminID, added_lift2);
            Assert.Equal(added_lift2, await facade.GetLiftInfoAsync(TestUsersCreator.skiPatrolID, added_lift2.LiftName));



            Assert.Empty(await facade.GetSlopesInfoAsync(TestUsersCreator.skiPatrolID));

            Slope added_slope1 = new Slope(1, "A1", true, 1);
            added_slope1 = await facade.AdminAddAutoIncrementSlopeAsync(TestUsersCreator.adminID, added_slope1);
            Slope added_slope2 = new Slope(2, "A2", false, 20);
            await facade.AdminAddSlopeAsync(TestUsersCreator.adminID, added_slope2);
            Slope added_slope3 = new Slope(3, "A3", true, 5);
            await facade.AdminAddSlopeAsync(TestUsersCreator.adminID, added_slope3);
            added_slope3 = new Slope(added_slope3.SlopeID, "A33", added_slope3.IsOpen, added_slope3.DifficultyLevel);
            await facade.UpdateSlopeInfoAsync(TestUsersCreator.skiPatrolID, added_slope3);
            Assert.Equal(added_slope3, await facade.GetSlopeInfoAsync(TestUsersCreator.skiPatrolID, added_slope3.SlopeName));


            Assert.Empty(await facade.GetLiftsSlopesInfoAsync(TestUsersCreator.skiPatrolID));
            LiftSlope added_lift_slope1 = new LiftSlope(1, added_lift1.LiftID, added_slope1.SlopeID);
            LiftSlope added_lift_slope2 = new LiftSlope(2, added_lift1.LiftID, added_slope2.SlopeID);
            LiftSlope added_lift_slope4 = new LiftSlope(4, added_lift2.LiftID, added_slope2.SlopeID);

            added_lift_slope1 = await facade.AdminAddAutoIncrementLiftSlopeAsync(TestUsersCreator.adminID, added_lift_slope1);
            await facade.AdminAddLiftSlopeAsync(TestUsersCreator.adminID, added_lift_slope2);
            await facade.AdminAddLiftSlopeAsync(TestUsersCreator.adminID, added_lift_slope4);



            List<Lift> from_slope1 = (await facade.GetSlopeInfoAsync(TestUsersCreator.unauthorizedID, added_slope1.SlopeName)).ConnectedLifts;
            Assert.Equal(1, from_slope1.Count);
            Assert.Equal(added_lift1, from_slope1[0]);

            List<Lift> from_slope2 = (await facade.GetSlopeInfoAsync(TestUsersCreator.skiPatrolID, added_slope2.SlopeName)).ConnectedLifts;
            Assert.Equal(2, from_slope2.Count);
            Assert.Equal(added_lift1, from_slope2[0]);
            Assert.Equal(added_lift2, from_slope2[1]);

            List<Lift> from_slope3 = (await facade.GetSlopeInfoAsync(TestUsersCreator.authorizedID, added_slope3.SlopeName)).ConnectedLifts;
            Assert.Equal(0, from_slope3.Count);



            List<Slope> from_lift1 = (await facade.GetLiftInfoAsync(TestUsersCreator.unauthorizedID, added_lift1.LiftName)).ConnectedSlopes;
            Assert.Equal(2, from_lift1.Count);
            Assert.Equal(added_slope1, from_lift1[0]);
            Assert.Equal(added_slope2, from_lift1[1]);

            List<Slope> from_lift2 = (await facade.GetLiftInfoAsync(TestUsersCreator.unauthorizedID, added_lift2.LiftName)).ConnectedSlopes;
            Assert.Equal(1, from_lift2.Count);
            Assert.Equal(added_slope2, from_lift2[0]);

            await facade.AdminDeleteLiftAsync(TestUsersCreator.adminID, added_lift1);
            await facade.AdminDeleteLiftAsync(TestUsersCreator.adminID, added_lift2);

            await facade.AdminDeleteSlopeAsync(TestUsersCreator.adminID, added_slope1);
            await facade.AdminDeleteSlopeAsync(TestUsersCreator.adminID, added_slope2);
            await facade.AdminDeleteSlopeAsync(TestUsersCreator.adminID, added_slope3);

            Assert.Empty(await facade.GetLiftsInfoAsync(TestUsersCreator.adminID));
            Assert.Empty(await facade.GetSlopesInfoAsync(TestUsersCreator.adminID));
            Assert.Empty(await facade.GetLiftsSlopesInfoAsync(TestUsersCreator.adminID));

        }
    }
}