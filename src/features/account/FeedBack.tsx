import * as React from "react";
import { useForm, Controller, FieldValues } from "react-hook-form";
import { Box, Button, Checkbox, FormControlLabel, FormLabel, MenuItem, Radio, RadioGroup, Select, Typography } from "@mui/material";
import AppTextInput from "../../app/components/AppTextInput";
import agent from "../../app/api/agent";  // ✅ Make sure this import exists

export default function FeedbackForm() {
  const { control, handleSubmit, formState: { isSubmitting } } = useForm();

  async function submitForm(data: FieldValues) {
    try {
      await agent.Feedback.add(data);  
      alert("Feedback submitted successfully!");
    } catch (error) {
      console.error("Error submitting feedback:", error);
    }
  }

  return (
    <form onSubmit={handleSubmit(submitForm)}>
      <Box sx={{ padding: 4, display: "flex", flexDirection: "column", gap: 2 }}>
        <Typography variant="h5">User Feedback Form</Typography>

        <FormLabel>Category</FormLabel>
        <Controller
          name="category"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <Select {...field} fullWidth>
              <MenuItem value="Product">Product</MenuItem>
              <MenuItem value="Service">Service</MenuItem>
              <MenuItem value="Support">Support</MenuItem>
            </Select>
          )}
        />

        <FormLabel>Rating</FormLabel>
        <Controller
          name="rating"
          control={control}
          defaultValue="5"
          render={({ field }) => (
            <RadioGroup {...field} row>
              <FormControlLabel value="1" control={<Radio />} label="1" />
              <FormControlLabel value="2" control={<Radio />} label="2" />
              <FormControlLabel value="3" control={<Radio />} label="3" />
              <FormControlLabel value="4" control={<Radio />} label="4" />
              <FormControlLabel value="5" control={<Radio />} label="5" />
            </RadioGroup>
          )}
        />

        <Controller
          name="bought"
          control={control}
          defaultValue={false}
          render={({ field }) => (
            <FormControlLabel control={<Checkbox {...field} checked={field.value} />} label="I bought this product" />
          )}
        />

        <AppTextInput control={control} name="comment" label="Your Feedback" multiline rows={4} />

        <Button type="submit" variant="contained" color="primary" disabled={isSubmitting}>
          Submit Feedback
        </Button>
      </Box>
    </form>
  );
}
