import React from "react";
import { TextField } from "@mui/material";
import "./inputoutlined.module.css";

export interface PropsOptions {
    label: string;
    value: any;
}

interface Props {
    label: string;
    register: any;
    errors: any;
    type: string;
	size?: "small" | "medium"
    fullWidth?: boolean | undefined;
    data: PropsOptions[];
    styles?: React.CSSProperties;
}

export const SelectOutlinedComponet: React.FC<Props> = ({ register, label, type, data, size, styles, errors, fullWidth }) => (
   
	<TextField
		style={styles}
		select
		SelectProps={{
			native: true,
		}}
		InputLabelProps={{
			shrink:true
		}}
		size={size ?? "small"}
		margin='normal'
		fullWidth={fullWidth}
		error={errors[register["name"]] ? true : false}
		helperText={errors[register["name"]] ? errors[register["name"]].message : ""}
		label={label}
		variant="outlined"
		type={type}
		{...register}
	>
		<option value={""}>
                Select ...
		</option>
		{data.map((item) => (
			<option key={item.value} value={item.value}>
				{item.label}
			</option>
		))}
	</TextField>
);

export default SelectOutlinedComponet;  