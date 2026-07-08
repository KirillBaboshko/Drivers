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
        return this.priceWithExtraCharge*this.extraCharge
    }
    public get priceWithExtraCharge(): number {
        return this.priceWithExtraCharge+this.priceExtraCharge;
    }
}
