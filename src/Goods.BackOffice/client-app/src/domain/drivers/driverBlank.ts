import { Gender } from "./driverGender";
import { RightsCategory } from "./driverRightCategory";
import { TransportVechile } from "../transportVechile/transportVechile";
import {Driver} from "./driver"
export class DriverBlank {
    constructor(
        public readonly id: string | null,
        public readonly name:string | null,
        public readonly surname:string | null,
        public readonly patronymic: string | null,
        public readonly gender: Gender | null,
        public readonly rightsCategories: RightsCategory[] | null,
        public readonly age: number | null,
        public readonly experience:number | null,
        public readonly transportVechile: TransportVechile | null,
        public readonly payment: number | null,
    ) { }
}
export namespace DriverBlank {
    export function getEmpty(): DriverBlank {
        return new DriverBlank(null, null, null, null, null,null,null,null,null,null);
    }

    export function getFromDriver(driver: Driver): DriverBlank {
        return {
            id: driver.id,
            name:driver.name,
            surname:driver.surname,
            patronymic:driver.patronymic,
            gender:driver.gender,
            rightsCategories:driver.rightsCategories,
            age:driver.age,
            experience:driver.experience,
            transportVechile:driver.transportVechile,
            payment:driver.payment
        };
    }
}