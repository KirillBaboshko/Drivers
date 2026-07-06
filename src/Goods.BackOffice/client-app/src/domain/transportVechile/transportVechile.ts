import {TransportType} from './transportVechileType';
import { Page } from '../../tools/types/page';
export class TransportVechile{
    constructor(
        public readonly id:number,
        public readonly type: TransportType,
        public readonly name:string,
        public readonly stateNumber:string,
        public readonly averageSpeed:number,
        public readonly fuelConsumption:number
    )
    {}
}
export const mapToTransportVechilesPage = (data: any):Page<TransportVechile> => 
    Page.convert(data,mapToTransportVechile);

export const mapToTransportVechiles = (data: any[]):TransportVechile[] => 
    data.map(mapToTransportVechile);

export const mapToTransportVechile = (data: any):TransportVechile => 
    new TransportVechile(data.id, data.type, data.name, data.stateNumber,data.averageSpeed,data.fuelConsumption);