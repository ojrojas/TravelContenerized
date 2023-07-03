import React from "react";
import { Checkbox, FormControl, FormControlLabel, FormGroup } from "@mui/material";

interface Props {
    label: string;
    register: any;
    labelPlacement?: "end" | "start" | "top" | "bottom" | undefined;
}

const CheckBoxComponent: React.FC<Props> = ({ label, labelPlacement, register }) => {
	return (
		<FormControl component="fieldset" variant="standard">
			<FormGroup>
				<FormControlLabel
					control={<Checkbox
						{...register} />} 
					label={label}
					labelPlacement={labelPlacement}                />
			</FormGroup>
		</FormControl>
	);
};

export default CheckBoxComponent;