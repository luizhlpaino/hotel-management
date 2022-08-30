import { Action } from "../enums/action.enum";
import { Status } from "../enums/status.enum";
import Room from "./room.entity";

export default class Booking {
    id: number;
    name: string;
    status: Status;
    room: Room;

    constructor() {
        this.status = Status.Created;
    }

    ChangeState(action: Action): void {
        switch(true) {
            case (this.status == Status.Created && action == Action.Pay):
                this.status = Status.Paid;
                break;
            case (this.status == Status.Paid && action == Action.Finish):
                this.status = Status.Finished;
                break;
            case (this.status == Status.Created && action == Action.Cancel):
                this.status = Status.Canceled;
                break;
            case (this.status == Status.Paid && action == Action.Refound):
                this.status = Status.Refounded;
                break;           
        }
    }
}