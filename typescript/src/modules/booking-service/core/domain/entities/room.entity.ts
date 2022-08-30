export default class Room {
    id: number;
    name: string;
    level: number;
    inMaintenance: boolean;    

    get hasGuest() {
        return true;
    }

    get isAvaiable() {
        return !this.inMaintenance || this.hasGuest;
    }
}