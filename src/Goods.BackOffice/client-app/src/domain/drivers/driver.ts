import { Page } from '../../tools/types/page';
import { Gender } from './driverGender';
import {RightsCategory} from './driverRightCategory'
import {TransportVechile} from '../transportVechile/transportVechile'

export class Driver {
    constructor(
        public readonly id: string,
        public readonly name:string,
        public readonly surname:string,
        public readonly patronymic: string | null,
        public readonly gender: Gender,
        public readonly rightsCategories: RightsCategory[],
        public readonly age: number,
        public readonly experience:number,
        public readonly transportVechile: TransportVechile,
        public readonly payment: number,
    ) { }
    public get fullName(): string {
		return [this.surname, this.name, this.patronymic].filter((part) => !String.isNullOrWhitespace(part)).join(' ');
	}
}

export function mapToDriversPage(data: any): Page<Driver> {
    return Page.convert(data, mapToDriver);
}

export function mapToDrivers(data: any[]): Driver[] {
    return data.map(mapToDriver);
}

export function mapToDriver(data: any): Driver {
    return new Driver(data.id,data.name,data.surname,data.patronymic, 
        data.gender, data.rightsCategories, data.age, data.experience, data.tranportVechile,data.payment);
}
