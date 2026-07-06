import React from 'react';
import { Route } from 'react-router-dom';
import { TransportVechilePage } from './transportVechilePage';

export function TransportVechilesRouter() {
	return (
		<>
			<Route path={TransportLink.index} element={<TransportVechilePage />} />
		</>
	);
}

export class TransportLink {
	public static index = '/transports';
}
