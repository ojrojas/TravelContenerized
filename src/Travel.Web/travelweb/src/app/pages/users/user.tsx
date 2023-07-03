import React from "react";
import { Button,  Divider, Grid, Paper } from "@mui/material";
import HeaderComponent from "../../components/headers/headerpage/header.component";
import { IUser } from "../../core/models/user/user";
import { useAppDispatch } from "../../hooks";
import ListUsersComponent from "./components/listusers.component";
import { getAllUsers } from "./redux/users-actions";
import DialogComponent from "../../components/modal/dialog.component";
import FormUserCreateComponent from "./components/formusercreate.component";

const UsersPage: React.FC = () => {
	const [open, setOpen] = React.useState<boolean>(false);
	const dispatch = useAppDispatch();
	const [user, setUser] = React.useState<IUser | undefined>(undefined);
	const [typeComponent, setTypeComponent] = React.useState<"CREATE" | "EDIT">("CREATE");

	React.useEffect(() => {
		dispatch(getAllUsers());
	}, []);

	const handleToggleModal = () => {
		setOpen(!open);
	};

	const handleCreateUser = () => {
		setTypeComponent("CREATE");
		setUser(undefined);
		handleToggleModal();
	};

	const handleEditOpenModal = (user: IUser) => {
		setUser(undefined);
		setUser(user);
		setTypeComponent("EDIT");
		handleToggleModal();
	};

	return (
		<Grid container sx={{ padding: 1, height: "100vh" }} gridRow={1}>
			<Grid item xs={12} md={12} lg={12} xl={12}>
				<Paper  elevation={4} sx={{ backgroundColor: "#fff", height: "99%",  padding:5 }}>
					<HeaderComponent namePage="Users" subNamePage="List Users" actionButtons={<Button variant="outlined" onClick={handleCreateUser}>Add</Button>}/>
					<Divider />
					<br/>
					<ListUsersComponent setEditUser={handleEditOpenModal} />
				</Paper>
			</Grid>

			<DialogComponent id={"modal-create-edit-users"}
				titleDialog={typeComponent === "EDIT" ? "Edit User" : "Create User"}
				onClose={() => {
					setOpen(false);
				}}
				open={open}
				maxWidth={"lg"}
				fullWidth={true}>
				<FormUserCreateComponent typeComponent={typeComponent} userExists={user} onClose={handleToggleModal} />
			</DialogComponent>
		</Grid>
	);
};

export default UsersPage;