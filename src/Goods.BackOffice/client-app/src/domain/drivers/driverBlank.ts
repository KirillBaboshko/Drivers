import { Gender } from "./driverGender";
import { RightsCategory } from "./driverRightCategory";
import {Driver} from "./driver"
export class DriverBlank {
    constructor(
        public id: string | null,
        public name:string | null,
        public surname:string | null,
        public patronymic: string | null,
        public gender: Gender | null,
        public rightsCategories: RightsCategory[],
        public age: number | null,
        public experience:number | null,
        public transportVechileId: string | null,
        public payment: number | null,
    ) { }
}
export namespace DriverBlank {
    export function getEmpty(): DriverBlank {
        return new DriverBlank(null, null, null, null, null,[],null,null,null,null);
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
            transportVechileId:driver.transportVechile.id,
            payment:driver.payment
        };
    }
}