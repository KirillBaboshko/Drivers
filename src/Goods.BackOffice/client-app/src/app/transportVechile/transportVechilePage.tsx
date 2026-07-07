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
import { TransportVechile } from '../../domain/transportVechile/transportVechile';
import { TransportType } from '../../domain/transportVechile/transportVechileType';
import { TransportVechilesProvider } from '../../domain/transportVechile/transportVechileProvider';
import { Button } from '../../shared/components/buttons/button';
import { ConfirmModal } from '../../shared/components/modals/confirmModal';
import { Notification } from '../../shared/components/notification';
import { TablePagination } from '../../shared/components/tablePagination';
import { ConfirmModalState } from '../../shared/types/confirmModalState';
import { Pagination } from '../../tools/types/pagination';
import { TransportVechileEditorModal } from './modals/transportVechileEditorModal';


type TranportEditorModalState = {
	transportId: string | null;
	isOpen: boolean;
};

interface RemoveTranportConfirmModalState extends ConfirmModalState {
	transportId: string | null;
}

export const TransportVechilePage=()=>{
        const [transports, setTransports] = useState<TransportVechile[]>([]);
        const [pagination, setPagination] = useState<Pagination>(Pagination.default);
    
        const [transportEditorModalState, setTransportEditorModalState] = useState<TranportEditorModalState >({
            transportId: null,
            isOpen: false
        });
        const [removeTransportConfirmModalState, setRemoveTransportConfirmModalState] =
            useState<RemoveTranportConfirmModalState>({ transportId: null, ...ConfirmModalState.getClosed() });
    
        const [errorMessage, setErrorMessage] = useState<string | null>(null);
    
        useEffect(() => {
            loadTransportsPage({ ...pagination });
        }, []);
    
        async function loadTransportsPage(newPagination: Pagination) {
            const transportsPage = await TransportVechilesProvider.getTransportVechilesPage(newPagination.page, newPagination.pageSize);
    
            setTransports(transportsPage.values);
            setPagination((pagination) => ({
                ...pagination,
                page: newPagination.page,
                pageSize: newPagination.pageSize,
                totalRows: transportsPage.totalRows
            }));
        }
    
        function openTransportEditorModal(transportId?: string) {
            setTransportEditorModalState({ transportId: transportId ?? null, isOpen: true });
        }
    
        function closeTransportEditorModal(isEdited: boolean) {
            if (isEdited) loadTransportsPage({ ...pagination, page: 1 });
            setTransportEditorModalState({ transportId: null, isOpen: false });
        }
    
        function openRemoveTransportConfirmModal(transportId: string, transportName: string) {
            setRemoveTransportConfirmModalState({
                transportId,
                ...ConfirmModalState.getOpen(`Вы действительно хотите удалить транспорт "${transportName}"`)
            });
        }
    
        async function closeRemoveTransportConfirmModal(isConfirmed: boolean) {
            if (isConfirmed) {
                if (removeTransportConfirmModalState.transportId == null) throw 'Cannot remove transport with transportId = null';
    
                const result = await TransportVechilesProvider.removeTransportVechile(removeTransportConfirmModalState.transportId);
                if (!result.isSuccess) {
                    setErrorMessage(result.errors.map((error) => error.message).join('. '));
                    return;
                }
    
                loadTransportsPage({ ...pagination, page: 1 });
            }
    
            setRemoveTransportConfirmModalState({ transportId: null, ...ConfirmModalState.getClosed() });
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
                    <Typography variant='h6'>Транспорт</Typography>
                    <Button variant='add' title='Создать' onClick={() => openTransportEditorModal()} />
                </Paper>
                <Paper elevation={3} sx={{ height: 'calc(100% - 52px)' }}>
                    <TableContainer sx={{ height: 'inherit' }}>
                        <Table stickyHeader>
                            <TableHead>
                                <TableRow>
                                    <TableCell>Тип транспорта</TableCell>
                                    <TableCell>Название</TableCell>
                                    <TableCell>Гос. номер</TableCell>
                                    <TableCell>Средняя скорость</TableCell>
                                    <TableCell>Расход топлива в час</TableCell>
                                    <TableCell>Управление</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {
                                    transports.length === 0 &&
                                    <TableRow>
                                        <TableCell colSpan={6}>Пусто</TableCell>
                                    </TableRow>
                                }
                                {
                                    transports.map(transport => (
                                        <TableRow key={`transport__${transport.id}`}>
                                            <TableCell width='15%'>
                                                {TransportType.getDisplayName(transport.type)}
                                            </TableCell>
                                            <TableCell width='30%'>{transport.name}</TableCell>
                                            <TableCell width='20%'>{transport.stateNumber}</TableCell>
                                            <TableCell width='20%'>{transport.averageSpeed}</TableCell>
                                            <TableCell width='15%'>{transport.fuelConsumption}</TableCell>
                                            <TableCell>
                                                <Button
                                                    type='icon'
                                                    variant='edit'
                                                    size='small'
                                                    onClick={() => openTransportEditorModal(transport.id)} />
                                                <Button
                                                    type='icon'
                                                    variant='remove'
                                                    size='small'
                                                    onClick={() => openRemoveTransportConfirmModal(transport.id, transport.name)} />
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
                        changePage={page => loadTransportsPage({ ...pagination, page })}
                        changeCountInPage={pageSize => loadTransportsPage({ ...pagination, pageSize })}
                    />
                </Paper>
    
                <TransportVechileEditorModal
                    isOpen={transportEditorModalState.isOpen}
                    transportId={transportEditorModalState.transportId}
                    onClose={closeTransportEditorModal}
                />
    
                <ConfirmModal
                    title={removeTransportConfirmModalState.title}
                    onClose={(isConfirmed) => closeRemoveTransportConfirmModal(isConfirmed)}
                    isOpen={removeTransportConfirmModalState.isOpen}
                />
    
                {
                    !String.isNullOrWhitespace(errorMessage) &&
                    <Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
                }
            </Container>
        );
}