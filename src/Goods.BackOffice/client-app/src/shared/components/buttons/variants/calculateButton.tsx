import CalculateIcon from '@mui/icons-material/Calculate';

import { Button, IconButton, Tooltip } from '@mui/material';
import React from 'react';
import { ButtonProps as DefaultProps } from '../button';

export interface Props extends DefaultProps {}

export function CalculateButton(props: Props) {
	switch (props.type) {
		case 'icon': {
			return (
				<Tooltip title={props.title ?? 'Рассчитать'} disableHoverListener={props.disableHoverListener}>
					<IconButton
						color={props.color ?? 'info'}
						size={props.size}
						sx={props.sx}
						className={props.className}
						onClick={props.onClick}
						disabled={props.disabled}>
						<CalculateIcon color={props.color ?? 'info'} fontSize={props.size} />
					</IconButton>
				</Tooltip>
			);
		}
		default: {
			return (
				<Button
					startIcon={<CalculateIcon fontSize={props.size} color={props.color ?? 'info'} />}
					variant={props.formVariant ?? 'outlined'}
					size={props.size}
					color={props.color ?? 'info'}
					sx={props.sx}
					className={props.className}
					onClick={props.onClick}
					disabled={props.disabled}>
					{props.title ?? 'Рассчитать'}
				</Button>
			);
		}
	}
}
