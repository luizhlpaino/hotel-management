import Booking from "../../../core/domain/entities/booking.entity";
import { Action } from "../../../core/domain/enums/action.enum";
import { Status } from "../../../core/domain/enums/status.enum";

describe("Booking Entity State-Machine", () => {
    test("Should change booking status to paid after executing pay action", () => {
        const booking = new Booking();
        booking.ChangeState(Action.Pay);
        expect(booking.status).toBe(Status.Paid);
    });
    test("Should change booking status to finished after executing pay and finish action", () => {
        const booking = new Booking();
        booking.ChangeState(Action.Pay);
        booking.ChangeState(Action.Finish);
        expect(booking.status).toBe(Status.Finished);
    });
})