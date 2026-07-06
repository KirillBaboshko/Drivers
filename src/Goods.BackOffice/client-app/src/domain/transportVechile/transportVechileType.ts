export enum TransportType{
    PassengerCar=1,
    Truck=2,
    Bus=3
}
export namespace TransportType{
    export const getDisplayName=(type:TransportType):string=>{
        switch(type)
        {
            case TransportType.PassengerCar:
                return 'Легковой автомобиль'
            case TransportType.Truck:
                return 'Грузовик'
            case TransportType.Bus:
                return 'Автобус'    
        }
    }
}