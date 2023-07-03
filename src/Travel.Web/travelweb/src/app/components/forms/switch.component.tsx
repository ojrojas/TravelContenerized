import { FormControl, FormControlLabel, FormGroup, Switch } from "@mui/material";
import React from "react";

interface Props {
    label: string;
    register: any;
    errors: any;
	size?: "small" | "medium" | undefined
    styles?: React.CSSProperties;
    labelPlacement?: "bottom" | "end" | "start" | "top";
    defaultValue?:boolean;
}

const SwitchCompontent: React.FC<Props> = ({ label, styles, labelPlacement,defaultValue,size, register }) => {
	return (
		<FormControl component="fieldset" variant="standard" margin="normal">
			<FormGroup>
				<FormControlLabel
					control={
						<Switch
							size={size ?? "small"}
							defaultChecked={defaultValue}
							style={styles}
							{...register} />
					}
					label={label}
					labelPlacement={labelPlacement}
				/>
			</FormGroup>
		</FormControl>
	);
};

export default SwitchCompontent;