export class TripCost {
    constructor(
        public readonly lengthOfWay: number,
        public readonly priceFuel: number,
        public readonly pricePayment: number,
        public readonly timeSpent: number,
        public readonly fuelSpent: number,
        public readonly extraCharge: number,
    ) { }
    public get priceFuelSpent():number{
        return this.priceFuel*this.fuelSpent
    }
    public get pricePaymentSpent():number{
        return this.pricePayment*this.timeSpent
    }
    public get priceWithoutExtraCharge(): number {
        return this.pricePaymentSpent+ this.priceFuelSpent;
    }
    public get priceExtraCharge(): number{
        return this.priceWithoutExtraCharge*this.extraCharge
    }
    public get priceWithExtraCharge(): number {
        return this.priceWithoutExtraCharge+this.priceExtraCharge;
    }
}
export function mapToTripCost(data: any): TripCost {
    return new TripCost(data.lengthOfWay,data.priceOfFuel,data.priceOfPayment,data.timeSpent,data.fuelSpent,data.extraCharge);
}