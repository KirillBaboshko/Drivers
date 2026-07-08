import { AppBar, Box, Typography } from '@mui/material';
import React from 'react';
import { Link, Outlet } from 'react-router-dom';
import { DriverLink } from '../../app/drivers/driersRouter';
//import { ProductLink } from '../../app/products/productsRouter';
import { TransportLink } from '../../app/transportVechile/transportVechileRouter';

const navLinkSx = {
	color: 'inherit',
	textDecoration: 'none',
	fontWeight: 500
};

export function Layout() {
	return (
		<>
			<AppBar position='fixed' sx={{ height: 64 }}>
				<Box
					sx={{
						display: 'flex',
						width: '100%',
						height: '100%',
						alignItems: 'center',
						gap: 4,
						paddingX: 2
					}}>
					<Box sx={{ width: 'fit-content', height: '100%', display: 'flex', alignItems: 'center' }}>
						<Typography sx={{ fontWeight: 'bold' }}>Goods</Typography>
					</Box>
					<Box sx={{ display: 'flex', alignItems: 'center', gap: 3 }}>
						{/* <Typography component={Link} to={ProductLink.index} sx={navLinkSx}>
							Продукты
						</Typography> */}
						<Typography component={Link} to={TransportLink.index} sx={navLinkSx}>
							Транспортные средства
						</Typography>
						<Typography component={Link} to={DriverLink.index} sx={navLinkSx}>
							Водители
						</Typography>
					</Box>
				</Box>
			</AppBar>
			<Box
				sx={{
					width: '100%',
					height: '100%',
					paddingTop: 10,
					paddingBottom: 2,
					paddingX: 2
				}}>
				<Outlet />
			</Box>
		</>
	);
}
