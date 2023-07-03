import React from "react";
import { InputBaseComponentProps, TextField } from "@mui/material";


interface Props {
    label: string;
    register: any;
    type: string;
    errors?: any;
    fullWidth?: boolean | undefined;
    shrinkProp?: boolean;
    styles?: React.CSSProperties;
    defaultValue?: string;
    size?: "small" | "medium";
    inputProps?: InputBaseComponentProps;
}

const InputOutlinedComponent: React.FC<Props> = ({ register, label, type, errors, fullWidth, shrinkProp, styles, size,inputProps,  defaultValue }) => (
	<TextField
		style={styles}
		margin='normal'
		size={size ?? "small"}
		fullWidth={fullWidth}
		error={errors[register["name"]] ? true : false}
		helperText={errors[register["name"]] ? errors[register["name"]].message : ""}
		label={label}
		defaultValue={defaultValue}
		variant="outlined"
		type={type}
		inputProps={inputProps}
		InputLabelProps={{
			shrink: shrinkProp
		}}
		{...register}
	/>
);

export default InputOutlinedComponent;