import { Table, TableBody, TableCell, TableRow, Typography } from '@mui/material';
import React from 'react';
import { TripCost } from '../../../domain/drivers/tripCost';
import { Button } from '../../../shared/components/buttons/button';
import { Modal } from '../../../shared/components/modals/modal';

interface Props {
	driverName: string | null;
	tripCost: TripCost | null;
	onClose: () => void;
	isOpen: boolean;
}

export function TripCostModal(props: Props) {
	return (
		<Modal onClose={props.onClose} isOpen={props.isOpen}>
			<Modal.Header onClose={props.onClose}>Расчёт стоимости рейса</Modal.Header>
			<Modal.Body sx={{ minWidth: '480px', display: 'flex', flexDirection: 'column', gap: '8px' }}>
				{props.tripCost != null && (
					<>
						{!String.isNullOrWhitespace(props.driverName) && (
							<Typography variant='subtitle1'>Водитель: {props.driverName}</Typography>
						)}
						<Table size='small'>
							<TableBody>
								<TableRow>
									<TableCell>Дистанция</TableCell>
									<TableCell align='right'>{props.tripCost.lengthOfWay} км</TableCell>
								</TableRow>
								<TableRow>
									<TableCell>Время в пути</TableCell>
									<TableCell align='right'>{props.tripCost.timeSpent} ч</TableCell>
								</TableRow>
                                <TableRow>
									<TableCell>Потрачено топлива</TableCell>
									<TableCell align='right'>{props.tripCost.fuelSpent} ₽</TableCell>
								</TableRow>
								<TableRow>
									<TableCell>Оплата водителю</TableCell>
									<TableCell align='right'>{props.tripCost.pricePaymentSpent} ₽</TableCell>
								</TableRow>
								<TableRow>
									<TableCell>Стоимость топлива</TableCell>
									<TableCell align='right'>{props.tripCost.priceFuelSpent} ₽</TableCell>
								</TableRow>
								<TableRow>
									<TableCell>Себестоимость</TableCell>
									<TableCell align='right'>{props.tripCost.priceWithoutExtraCharge} ₽</TableCell>
								</TableRow>
								<TableRow>
									<TableCell>Наценка компании</TableCell>
									<TableCell align='right'>{props.tripCost.extraCharge} ₽</TableCell>
								</TableRow>
								<TableRow>
									<TableCell sx={{ fontWeight: 'bold' }}>Итоговая стоимость</TableCell>
									<TableCell align='right' sx={{ fontWeight: 'bold' }}>
										{props.tripCost.priceWithExtraCharge} ₽
									</TableCell>
								</TableRow>
							</TableBody>
						</Table>
					</>
				)}
			</Modal.Body>
			<Modal.Footer>
				<Button variant='close' title='Закрыть' onClick={props.onClose} />
			</Modal.Footer>
		</Modal>
	);
}