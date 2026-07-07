import React from 'react';
import { Route } from 'react-router-dom';
import { DriversPage } from './driversPage';

export function DriversRouter() {
	return (
		<>
			<Route path={DriverLink.index} element={<DriversPage />} />
		</>
	);
}

export class DriverLink {
	public static index = '/drivers';
}