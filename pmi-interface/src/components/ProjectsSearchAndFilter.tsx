import {
  Button,
  Card,
  FormControl,
  InputAdornment,
  OutlinedInput,
  styled,
  ToggleButton,
  ToggleButtonGroup,
  toggleButtonGroupClasses,
  Toolbar,
  toggleButtonClasses,
  Typography,
  useTheme,
} from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";
import FilterAltIcon from "@mui/icons-material/FilterAlt";
import React, { useState } from "react";
import type {} from "@mui/material/themeCssVarsAugmentation";

const searchPlaceHolder =
  "Search projects by name, description, or collaborator...";

const ProjectsSearchAndFilter = () => {
  const [filtersActive, setFiltersActive] = useState(false);
  const [alignment, setAlignment] = React.useState<string | null>("left");
  const handleAlignment = (
    event: React.MouseEvent<HTMLElement>,
    newAlignment: string | null
  ) => {
    setAlignment(newAlignment);
  };
  const theme = useTheme();

  const FilterBurro = (
    <Button
      variant="outlined"
      disableRipple
      startIcon={<FilterAltIcon />}
      onClick={() => setFiltersActive(!filtersActive)}
      sx={{
        color: filtersActive
          ? theme.palette.primary.main
          : theme.palette.text.disabled,
        backgroundColor: filtersActive
          ? "#` `"
          : theme.palette.background.default,
        borderColor: filtersActive
          ? theme.palette.primary.main
          : theme.palette.text.disabled,
        "&:hover": {
          borderColor: filtersActive
            ? theme.palette.primary.main
            : theme.palette.text.disabled,
          backgroundColor: "transparent",
          transition: "all 0.2s ease",
        },
      }}
    >
      Filters
    </Button>
  );

  return (
    <Toolbar
      disableGutters
      sx={{
        marginTop: 3,
        display: "flex",
        flexDirection: "column",
        padding: 0,
      }}
    >
      <FormControl
        variant="outlined"
        fullWidth
        sx={{ display: "flex", flexDirection: "row  " }}
      >
        <OutlinedInput
          required
          placeholder={searchPlaceHolder}
          sx={outlinedInputStyles}
          startAdornment={
            <InputAdornment position="start">
              <SearchIcon
                sx={{ color: theme.palette.text.disabled, scale: 1.2 }}
              />
            </InputAdornment>
          }
        />
        {FilterBurro}
      </FormControl>
      {filtersActive && (
        <Card sx={filterBoxStyles} variant="outlined">
          <Typography
            variant="body1"
            sx={{ display: "inline-block", marginRight: 2 }}
          >
            Sort by:
          </Typography>

          <StyledToggleButtonGroup
            value={alignment}
            exclusive
            onChange={handleAlignment}
          >
            <ContainedToggleButton size="small" value="left">
              Last Updated
            </ContainedToggleButton>
            <ContainedToggleButton size="small" value="center">
              Date Created
            </ContainedToggleButton>
            <ContainedToggleButton size="small" value="justify">
              Name
            </ContainedToggleButton>
          </StyledToggleButtonGroup>
        </Card>
      )}
    </Toolbar>
  );
};

const outlinedInputStyles: React.CSSProperties = {
  backgroundColor: "#1f1f1f",
  flex: 1,
  marginRight: 2,
};

const filterBoxStyles: React.CSSProperties = {
  width: "100%",
  marginTop: 2,
  padding: 2,
  borderRadius: 2,
};

const ContainedToggleButton = styled(ToggleButton)(({ theme }) => ({
  backgroundColor: "#0a0a0a",
  color: theme.palette.text.secondary,
  textTransform: "capitalize",
  paddingTop: 4,
  paddingBottom: 4,
  "&:hover": {
    backgroundColor: theme.palette.primary.dark,
    color: theme.palette.primary.contrastText,
  },
  "&.Mui-selected": {
    backgroundColor: theme.palette.primary.main,
    color: theme.palette.primary.contrastText,
  },
}));

const StyledToggleButtonGroup = styled(ToggleButtonGroup)(({ theme }) => ({
  gap: "2rem",
  [`& .${toggleButtonGroupClasses.firstButton}, & .${toggleButtonGroupClasses.middleButton}`]:
    {
      borderTopRightRadius: (theme.vars || theme).shape.borderRadius,
      borderBottomRightRadius: (theme.vars || theme).shape.borderRadius,
    },
  [`& .${toggleButtonGroupClasses.lastButton}, & .${toggleButtonGroupClasses.middleButton}`]:
    {
      borderTopLeftRadius: (theme.vars || theme).shape.borderRadius,
      borderBottomLeftRadius: (theme.vars || theme).shape.borderRadius,
      borderLeft: `1px solid ${(theme.vars || theme).palette.divider}`,
    },
  [`& .${toggleButtonGroupClasses.lastButton}.${toggleButtonClasses.disabled}, & .${toggleButtonGroupClasses.middleButton}.${toggleButtonClasses.disabled}`]:
    {
      borderLeft: `1px solid ${
        (theme.vars || theme).palette.action.disabledBackground
      }`,
    },
}));
export default ProjectsSearchAndFilter;
