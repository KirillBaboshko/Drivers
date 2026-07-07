import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { AppBase } from '../shared/components/appBase';
import { Layout } from '../shared/components/layout';
import { InfrastructureRouter } from './infrastructure/infrastructureRouter';
import { ProductsRouter } from './products/productsRouter';
import { TransportVechilesRouter } from './transportVechile/transportVechileRouter';
import { DriversRouter } from './drivers/driersRouter';

export function App() {
	return (
		<AppBase>
			<BrowserRouter>
				<Routes>
					<Route element={<Layout />}>
						{InfrastructureRouter()}
						{ProductsRouter()}
						{TransportVechilesRouter()}
						{DriversRouter()}
					</Route>
				</Routes>
			</BrowserRouter>
		</AppBase>
	);
}
