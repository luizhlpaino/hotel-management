import { Column, PrimaryKey } from "sequelize-typescript";
import { Model } from "sequelize/types";
import Room from "../../../core/domain/entities/room.entity";
import { Status } from "../../../core/domain/enums/status.enum";

export default class BookingModel extends Model {
    @PrimaryKey
    @Column
    id: number;
    name: string;
    status: Status;
    room: Room;
}