import { TransportVechile } from './transportVechile';
import { TransportType } from './transportVechileType';

export class TransportVechileBlank {
    constructor(
        public readonly id: number | null,
        public readonly type: TransportType | null,
        public readonly name:string | null,
        public readonly stateNumber:string | null,
        public readonly averageSpeed:number | null,
        public readonly fuelConsumption:number| null
    ) { }
}

export namespace TransportVechileBlank {
    export function getEmpty(): TransportVechileBlank {
        return new TransportVechileBlank(null, null, null, null, null,null);
    }

    export function getFromTransportVechile(transport: TransportVechile): TransportVechileBlank {
        return {
            id: transport.id,
            type:transport.type,
            name:transport.name,
            stateNumber:transport.stateNumber,
            averageSpeed:transport.averageSpeed,
            fuelConsumption:transport.fuelConsumption
        };
    }
}