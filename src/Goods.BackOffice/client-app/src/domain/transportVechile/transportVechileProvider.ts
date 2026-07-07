import { Page } from '../../tools/types/page';
import { mapToResult, Result } from '../../tools/types/results/result';
import { mapToTransportVechile, mapToTransportVechilesPage, TransportVechile } from './transportVechile';
import { TransportVechileBlank } from './transportVechileBlank';

export class TransportVechilesProvider {
    private static readonly headers: HeadersInit = [
        ['X-Requested-With', 'XMLHttpRequest'],
        ['Content-Type', 'application/json']
    ];

    public static async saveTransportVechile(transportBlank: TransportVechileBlank): Promise<Result> {
        const response = await fetch('/transports/save', {
            method: 'POST',
            headers: this.headers,
            body: JSON.stringify(transportBlank)
        });
        const json = await response.json();

        return mapToResult(json);
    }

    public static async getTransportVechilesPage(page: number, count: number): Promise<Page<TransportVechile>> {
        const response = await fetch(`/transports/get-page?page=${page}&count=${count}`, {
            method: 'GET',
            headers: this.headers
        });
        const json = await response.json();

        return mapToTransportVechilesPage(json);
    }
    public static async getAllTransportVehicles(): Promise<TransportVechile[]> {
		const page = await this.getTransportVechilesPage(1, 1000);

		return page.values;
	}

    public static async getTransportVechileById(id: string): Promise<TransportVechile | null> {
        const response = await fetch(`/transports/get-by-id?transportId=${id}`, {
            method: 'GET',
            headers: this.headers
        });
        const json = await response.json();

        return mapToTransportVechile(json);
    }

    public static async removeTransportVechile(id: string): Promise<Result> {
        const response = await fetch(`/transports/remove?transportId=${id}`, {
            method: 'GET',
            headers: this.headers
        });
        const json = await response.json();

        return mapToResult(json);
    }
}
