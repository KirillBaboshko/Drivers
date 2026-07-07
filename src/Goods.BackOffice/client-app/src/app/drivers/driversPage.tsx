import {
	Container,
	Paper,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableHead,
	TableRow,
	Typography
} from '@mui/material';
import React, { useEffect, useState } from 'react';
import { Driver } from '../../domain/drivers/driver';
import { RightsCategory } from '../../domain/drivers/driverRightCategory';
import { Gender } from '../../domain/drivers/driverGender';
import { DriversProvider } from '../../domain/drivers/driversProvider';
import { Button } from '../../shared/components/buttons/button';
import { ConfirmModal } from '../../shared/components/modals/confirmModal';
import { Notification } from '../../shared/components/notification';
import { TablePagination } from '../../shared/components/tablePagination';
import { ConfirmModalState } from '../../shared/types/confirmModalState';
import { Pagination } from '../../tools/types/pagination';
import { DriverEditorModal } from './modals/driversEditorModal';
import { TransportType } from '../../domain/transportVechile/transportVechileType';

type DriverEditorModalState = {
	driverId: string | null;
	isOpen: boolean;
};

interface RemoveDriverConfirmModalState extends ConfirmModalState {
	driverId: string | null;
}
export const DriversPage=()=>{
        const [drivers, setDrivers] = useState<Driver[]>([]);
        const [pagination, setPagination] = useState<Pagination>(Pagination.default);
    
        const [driverEditorModalState, setDriverEditorModalState] = useState<DriverEditorModalState>({
            driverId: null,
            isOpen: false
        });
        const [removeDriverConfirmModalState, setRemoveDriverConfirmModalState] =
            useState<RemoveDriverConfirmModalState>({ driverId: null, ...ConfirmModalState.getClosed() });
    
        const [errorMessage, setErrorMessage] = useState<string | null>(null);
    
        useEffect(() => {
            loadDriversPage({ ...pagination });
        }, []);
    
        async function loadDriversPage(newPagination: Pagination) {
            const driversPage = await DriversProvider.getPageDrivers(newPagination.page, newPagination.pageSize);
    
            setDrivers(driversPage.values);
            setPagination((pagination) => ({
                ...pagination,
                page: newPagination.page,
                pageSize: newPagination.pageSize,
                totalRows: driversPage.totalRows
            }));
        }
    
        function openDriverEditorModal(driverId?: string) {
            setDriverEditorModalState({ driverId: driverId ?? null, isOpen: true });
        }
    
        function closeDriverEditorModal(isEdited: boolean) {
            if (isEdited) loadDriversPage({ ...pagination, page: 1 });
            setDriverEditorModalState({ driverId: null, isOpen: false });
        }
    
        function openRemoveDriverConfirmModal(driverId: string, driverFullname: string) {
            setRemoveDriverConfirmModalState({
                driverId,
                ...ConfirmModalState.getOpen(`Вы действительно хотите удалить водителя "${driverFullname}"`)
            });
        }
    
        async function closeRemoveDriverConfirmModal(isConfirmed: boolean) {
            if (isConfirmed) {
                if (removeDriverConfirmModalState.driverId == null) throw 'Cannot remove driver with driverId = null';
    
                const result = await DriversProvider.removeDriver(removeDriverConfirmModalState.driverId);
                if (!result.isSuccess) {
                    setErrorMessage(result.errors.map((error) => error.message).join('. '));
                    return;
                }
    
                loadDriversPage({ ...pagination, page: 1 });
            }
    
            setRemoveDriverConfirmModalState({ driverId: null, ...ConfirmModalState.getClosed() });
        }
    
        return (
            <Container
                sx={{ height: '100%', display: 'flex', flexDirection: 'column', gap: '12px' }}
                maxWidth={false}
                disableGutters>
                <Paper
                    elevation={3}
                    sx={{
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'space-between',
                        paddingX: '12px',
                        paddingY: '6px'
                    }}>
                    <Typography variant='h6'>Водители</Typography>
                    <Button variant='add' title='Создать' onClick={() => openDriverEditorModal()} />
                </Paper>
                <Paper elevation={3} sx={{ height: 'calc(100% - 52px)' }}>
                    <TableContainer sx={{ height: 'inherit' }}>
                        <Table stickyHeader>
                            <TableHead>
                                <TableRow>
                                    <TableCell>ФИО</TableCell>
								    <TableCell>Пол</TableCell>
								    <TableCell>Категории прав</TableCell>
								    <TableCell>Возраст</TableCell>
								    <TableCell>Стаж</TableCell>
								    <TableCell>Транспортное средство</TableCell>
								    <TableCell>Оплата в час</TableCell>
								    <TableCell>Управление</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {
                                    drivers.length === 0 &&
                                    <TableRow>
                                        <TableCell colSpan={8}>Пусто</TableCell>
                                    </TableRow>
                                }
                                {
                                    drivers.map(driver => (
                                        <TableRow key={`driver__${driver.id}`}>
                                            <TableCell width='15%'>
                                                {driver.fullName}
                                            </TableCell>
                                            <TableCell width='20%'>{Gender.getDisplayName(driver.gender)}</TableCell>
                                            <TableCell width='20%'>{driver.rightsCategories.map(RightsCategory.getShortName).join(', ')}</TableCell>
                                            <TableCell width='40%'>{driver.age}</TableCell>
                                            <TableCell width='40%'>{driver.experience}</TableCell>
                                            <TableCell width='40%'>{TransportType.getDisplayName(driver.transportVechile.type)}
											{' — '}
											{driver.transportVechile.name}</TableCell>
                                            <TableCell width='15%'>{driver.payment}</TableCell>
                                            <TableCell>
                                                <Button
                                                    type='icon'
                                                    variant='edit'
                                                    size='small'
                                                    onClick={() => openDriverEditorModal(driver.id)} />
                                                <Button
                                                    type='icon'
                                                    variant='remove'
                                                    size='small'
                                                    onClick={() => openRemoveDriverConfirmModal(driver.id, driver.name)} />
                                            </TableCell>
                                        </TableRow>
                                    ))
                                }
                            </TableBody>
                        </Table>
                    </TableContainer>
    
                    <TablePagination
                        countInPageOptions={Pagination.pageSizeOptions}
                        page={pagination.page}
                        countInPage={pagination.pageSize}
                        totalRows={pagination.totalRows}
                        changePage={page => loadDriversPage({ ...pagination, page })}
                        changeCountInPage={pageSize => loadDriversPage({ ...pagination, pageSize })}
                    />
                </Paper>
    
                <DriverEditorModal
                    isOpen={driverEditorModalState.isOpen}
                    driverId={driverEditorModalState.driverId}
                    onClose={closeDriverEditorModal}
                />
    
                <ConfirmModal
                    title={removeDriverConfirmModalState.title}
                    onClose={(isConfirmed) => closeRemoveDriverConfirmModal(isConfirmed)}
                    isOpen={removeDriverConfirmModalState.isOpen}
                />
    
                {
                    !String.isNullOrWhitespace(errorMessage) &&
                    <Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
                }
            </Container>
        );
}