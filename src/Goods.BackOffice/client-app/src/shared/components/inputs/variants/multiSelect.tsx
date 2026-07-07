import { Autocomplete as MuiAutocomplete, TextField } from '@mui/material';
import React from 'react';
import { InputProps } from '../input';

export interface Props<TValue> extends InputProps {
	placeholder?: string;

	options: TValue[];
	getOptionLabel: (option: TValue) => string;
	isOptionEqualToValue?: (first: TValue, second: TValue) => boolean;

	value: TValue[];
	onChange: (value: TValue[]) => void;

	required?: boolean;
}

export function MultiSelect<TValue>(props: Props<TValue>) {
	function isOptionEqualToValue(first: TValue, second: TValue) {
		if (props.isOptionEqualToValue != undefined) return props.isOptionEqualToValue(first, second);

		return first === second;
	}

	return (
		<MuiAutocomplete
			multiple
			size={props.size}
			className={props.className}
			sx={props.sx}
			getOptionLabel={props.getOptionLabel}
			isOptionEqualToValue={isOptionEqualToValue}
			noOptionsText='Пусто'
			options={props.options}
			value={props.value}
			onChange={(_, value) => props.onChange(value as TValue[])}
			disabled={props.disabled}
			renderInput={(params) => (
				<TextField
					{...params}
					label={props.title}
					placeholder={props.placeholder}
					required={props.required && props.value.length === 0}
				/>
			)}
		/>
	);
}