import React, { useEffect, useState } from 'react';
import { TransportVechile } from '../../../domain/transportVechile/transportVechile';
import { TransportVechileBlank } from '../../../domain/transportVechile/transportVechileBlank';
import { TransportType} from '../../../domain/transportVechile/transportVechileType';
import { TransportVechilesProvider } from '../../../domain/transportVechile/transportVechileProvider';
import { Button } from '../../../shared/components/buttons/button';
import { Input } from '../../../shared/components/inputs/input';
import { Modal } from '../../../shared/components/modals/modal';
import { Notification } from '../../../shared/components/notification';
import { Enum } from '../../../tools/types/enum';

interface Props {
    transportId: string | null;
    onClose: (isEdited: boolean) => void;
    isOpen: boolean;
}

export const TransportVechileEditorModal=(props: Props)=> {
    const [transportBlank, setTransportVechileBlank] = useState<TransportVechileBlank>(TransportVechileBlank.getEmpty());
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    useEffect(() => {
        if (!props.isOpen) return;

        async function loadTransportVechileBlank() {
            let transportBlank: TransportVechileBlank | null = null;

            if (props.transportId != null) {
                const transport: TransportVechile | null = await TransportVechilesProvider.getTransportVechileById(props.transportId);
                if (transport == null) throw 'TransportVechile is null';

                transportBlank = TransportVechileBlank.getFromTransportVechile(transport);
            }

            setTransportVechileBlank(transportBlank ?? TransportVechileBlank.getEmpty());
        }

        loadTransportVechileBlank();

        return () => {
            setTransportVechileBlank(TransportVechileBlank.getEmpty());
            setErrorMessage(null);
        };
    }, [props.isOpen, props.transportId]);

    async function saveTransportVechile() {
        const result = await TransportVechilesProvider.saveTransportVechile(transportBlank);
        if (!result.isSuccess) {
            setErrorMessage(result.errorsAsString);
            return;
        }

        props.onClose(true);
    }

    return (
        <>
            <Modal onClose={() => props.onClose(false)} isOpen={props.isOpen}>
                <Modal.Header onClose={() => props.onClose(false)}>Редактор Транспортного средства</Modal.Header>
                <Modal.Body
                    sx={{
                        maxWidth: '800px',
                        minWidth: '600px',
                        display: 'flex',
                        flexDirection: 'column',
                        gap: '12px'
                    }}>
                    <Input
                        variant='select'
                        title='Выберите тип ТС'
                        options={Enum.getNumberValues<TransportType>(TransportType)}
                        getOptionLabel={(option) => TransportType.getDisplayName(option)}
                        isOptionEqualToValue={(a, b) => a === b}
                        value={transportBlank.type}
                        onChange={(type) => setTransportVechileBlank((transportBlank) => ({ ...transportBlank, type }))}
                        required
                    />
                    <Input
                        variant='text'
                        title='Введите название'
                        value={transportBlank.name}
                        onChange={(name) => setTransportVechileBlank((transportBlank) => ({ ...transportBlank, name }))}
                        required
                    />
                    <Input
                        variant='text'
                        title='Введите номер ТС'
                        value={transportBlank.stateNumber}
                        onChange={(stateNumber) =>
                            setTransportVechileBlank((transportBlank) => ({ ...transportBlank, stateNumber }))
                        }
                    />
                    <Input
                        variant='number'
                        title='Введите среднюю скорость ТС'
                        value={transportBlank.averageSpeed}
                        onChange={(averageSpeed) => setTransportVechileBlank((transportBlank) => ({ ...transportBlank, averageSpeed }))}
                        isAvailableFractionValue
                        required
                    />
                    <Input
                        variant='number'
                        title='Введите средний расход топлива ТС'
                        value={transportBlank.fuelConsumption}
                        onChange={(fuelConsumption) => setTransportVechileBlank((transportBlank) => ({ ...transportBlank, fuelConsumption }))}
                        isAvailableFractionValue
                        required
                    />
                </Modal.Body>
                <Modal.Footer>
                    <Button variant='save' onClick={() => saveTransportVechile()} />
                </Modal.Footer>
            </Modal>

            {
                !String.isNullOrWhitespace(errorMessage) &&
                <Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
            }
        </>
    );
}
