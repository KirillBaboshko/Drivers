import React, { useEffect, useState } from 'react';
import { Driver } from '../../../domain/drivers/driver';
import { DriverBlank } from '../../../domain/drivers/driverBlank';
import { Gender} from '../../../domain/drivers/driverGender';
import { DriversProvider } from '../../../domain/drivers/driversProvider';
import { RightsCategory} from '../../../domain/drivers/driverRightCategory';
import { Button } from '../../../shared/components/buttons/button';
import { Input } from '../../../shared/components/inputs/input';
import { Modal } from '../../../shared/components/modals/modal';
import { Notification } from '../../../shared/components/notification';
import { Enum } from '../../../tools/types/enum';

interface Props {
    driverId: string | null;
    onClose: (isEdited: boolean) => void;
    isOpen: boolean;
}

export const DriverEditorModal=(props: Props)=> {
    const [driverBlank, setDriverBlank] = useState<DriverBlank>(DriverBlank.getEmpty());
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    useEffect(() => {
        if (!props.isOpen) return;

        async function loadDriverBlank() {
            let driverBlank: DriverBlank | null = null;

            if (props.driverId != null) {
                const driver: Driver | null = await DriversProvider.getDriverById(props.driverId);
                if (driver == null) throw 'Driver is null';

                driverBlank = DriverBlank.getFromDriver(driver);
            }

            setDriverBlank(driverBlank ?? DriverBlank.getEmpty());
        }

        loadDriverBlank();

        return () => {
            setDriverBlank(DriverBlank.getEmpty());
            setErrorMessage(null);
        };
    }, [props.isOpen, props.driverId]);

    async function saveDriver() {
        const result = await DriversProvider.saveDrivers(driverBlank);
        if (!result.isSuccess) {
            setErrorMessage(result.errorsAsString);
            return;
        }

        props.onClose(true);
    }

    return (
        <>
            <Modal onClose={() => props.onClose(false)} isOpen={props.isOpen}>
                <Modal.Header onClose={() => props.onClose(false)}>Редактор Водителя</Modal.Header>
                <Modal.Body
                    sx={{
                        maxWidth: '800px',
                        minWidth: '600px',
                        display: 'flex',
                        flexDirection: 'column',
                        gap: '12px'
                    }}>
                    <Input
                        variant='text'
                        title='Введите имя'
                        value={driverBlank.name}
                        onChange={(name) => setDriverBlank((driverBlank) => ({ ...driverBlank, name }))}
                        required
                    />
                    <Input
                        variant='text'
                        title='Введите фамилию'
                        value={driverBlank.surname}
                        onChange={(surname) => setDriverBlank((driverBlank) => ({ ...driverBlank, surname }))}
                        required
                    />
                    <Input
                        variant='text'
                        title='Введите отчество'
                        value={driverBlank.patronymic}
                        onChange={(patronymic) => setDriverBlank((driverBlank) => ({ ...driverBlank, patronymic }))}
                        required
                    />
                    <Input
                        variant='select'
                        title='Выберите пол водителя'
                        options={Enum.getNumberValues<Gender>(Gender)}
                        getOptionLabel={(option) => Gender.getDisplayName(option)}
                        isOptionEqualToValue={(a, b) => a === b}
                        value={driverBlank.gender}
                        onChange={(gender) => setDriverBlank((driverBlank) => ({ ...driverBlank, gender }))}
                        required
                    />
                    
                    <Input
                        variant='text'
                        title='Введите номер ТС'
                        value={driverBlank.stateNumber}
                        onChange={(stateNumber) =>
                            setDriverBlank((driverBlank) => ({ ...driverBlank, stateNumber }))
                        }
                    />
                    <Input
                        variant='number'
                        title='Введите среднюю скорость ТС'
                        value={driverBlank.averageSpeed}
                        onChange={(averageSpeed) => setDriverBlank((driverBlank) => ({ ...driverBlank, averageSpeed }))}
                        isAvailableFractionValue
                        required
                    />
                    <Input
                        variant='number'
                        title='Введите средний расход топлива ТС'
                        value={driverBlank.fuelConsumption}
                        onChange={(fuelConsumption) => setDriverBlank((driverBlank) => ({ ...driverBlank, fuelConsumption }))}
                        isAvailableFractionValue
                        required
                    />
                </Modal.Body>
                <Modal.Footer>
                    <Button variant='save' onClick={() => saveDriver()} />
                </Modal.Footer>
            </Modal>

            {
                !String.isNullOrWhitespace(errorMessage) &&
                <Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
            }
        </>
    );
}
