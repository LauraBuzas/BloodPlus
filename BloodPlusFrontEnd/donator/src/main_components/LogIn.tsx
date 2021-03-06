import * as React from 'react';
import { VBox, HBox } from 'react-stylesheet';
import { TextField } from '../utils/TextField';
import update from 'react-addons-update';

import { IUserRegister } from './../Models/IUserRegister';
import { AccountService } from '../Services/AccountServices';
import { Redirect } from 'react-router';
import Alert from 'react-s-alert';
import '../css/SignUp.css';
import '../css/Button.css';
import { IUserLogin } from '../Models/IUserLogin';
import { DoctorRequest } from '../Components/Doctor/DoctorRequest/DoctorRequest';
//import WebSocketService from '../Services/WebSocketService';
import {ImgSource} from '../Components/ImgSource/ImgSource';
export interface LoginProps {
    setRole:any
}

interface LoginState {
    userRegistered: IUserLogin
    message: string
    role:string
}

export class LogIn extends React.Component<LoginProps, LoginState>
{
    constructor(props: LoginProps) {
        super(props);

        this.state =
            {
                message: '',
                userRegistered:
                    {
                        email: '',
                        password: 'Password123.'
                    },
                role:''
            }
    }

    handleEmailChange(event: any) {
        this.setState({
            userRegistered: update(this.state.userRegistered, { email: { $set: event.target.value } })
        });
    }

    handlePasswordChange(event: any) {
        this.setState({
            userRegistered: update(this.state.userRegistered, { password: { $set: event.target.value } })
        });
    }

    loginUser(event: any) {
        event.preventDefault();
        let user = {
            email: this.state.userRegistered.email,
            password: this.state.userRegistered.password
        }
        console.log(user);
        AccountService.loginUser(user).then((resp) => {
            this.setState({role:resp.data[0]});
            this.props.setRole(this.state.role);
        },
            (error) => {

                this.setState({
                    message: "Error logging in,please try again"
                });
                Alert.error(this.state.message, {
                    position: 'top-right',
                    effect: 'jelly'
                  });
            });
    }

    componentDidMount()
    {
        
    }

    render() {
        
        if(this.state.role=="HospitalAdmin")
        {
            return <Redirect to="/hospital/admin"/>
        }

        if(this.state.role=="DonationCenterAdmin")
        {
            return <Redirect to="/center/admin"/>
        }

        if(this.state.role=="HospitalDoctor")
        {
            return <Redirect to="/request"/>
        }

        if(this.state.role=="DonationCenterDoctor")
        {
           
            return <Redirect to="/employee/requests"/> 
        }
        
        if(this.state.role=="Donor")
        {
            return <Redirect to="/"/>
        }
        
        
        return (
            <div id="login-main">
                <div id="login-div">
                    <TextField text="Email" type="text" onChangeFunction={(event) => this.handleEmailChange(event)} />
                    <TextField text="Parolă" type="password" onChangeFunction={(event) => this.handlePasswordChange(event)} />
                    <button className="generic-button" onClick={(event) => this.loginUser(event)}>Conectare</button>
                    <Alert stack={true} timeout={3000} />
                </div>
                <ImgSource source="www.stiridecluj.ro" white={true} />
            </div>
            
        );
    }
}