import { Page } from '../../tools/types/page';
import { mapToResult, Result } from '../../tools/types/results/result';
import { mapToDriver, mapToDriversPage, Driver } from './driver';
import { DriverBlank } from './driverBlank';

export class DriversProvider{
    private static readonly headers: HeadersInit = [
		['X-Requested-With', 'XMLHttpRequest'],
		['Content-Type', 'application/json']
	];
    public static async saveDrivers(driverBlank:DriverBlank): Promise<Result>{
        const response = await fetch('/drivers/save', {
			method: 'POST',
			headers: this.headers,
			body: JSON.stringify(driverBlank)
		});
        const json = await response.json();

		return mapToResult(json);
    }
    public static async getPageDrivers(page: number, count: number):Promise<Page<Driver>>{
    const response=await fetch(`/transports/get-page?page=${page}&count=${count}`,{
        method:'GET',
        headers:this.headers
    });
    const json=await response.json();
    return mapToDriversPage(json);
    }
    
    public static async getDriverById(id: string): Promise<Driver | null> {
            const response = await fetch(`/drivers/get-by-id?DriverId=${id}`, {
                method: 'GET',
                headers: this.headers
            });
            const json = await response.json();
    
            return mapToDriver(json);
        }
    
        public static async removeDriver(id: string): Promise<Result> {
            const response = await fetch(`/drivers/remove?driverId=${id}`, {
                method: 'POST',
                headers: this.headers
            });
            const json = await response.json();
    
            return mapToResult(json);
        }

}
