import { TextField } from "@mui/material";
import { use } from "react";
import { useController, UseControllerProps } from "react-hook-form";

interface Props extends UseControllerProps {
    label : string;
    multiline?: boolean;
    rows?: number;
    type?: string;
}

export default function AppTextInput(props: Props) {
  const { field,fieldState } = useController({...props,defaultValue: ''});
  return (
    <TextField
        {...props}
        {...field}
        multiline={props.multiline}
        rows={props.rows}
        type={props.type}
        fullWidth
        variant="outlined"
        error={!!fieldState.invalid}
        helperText={fieldState.error?.message}
        required = {true}
    />
  );
}