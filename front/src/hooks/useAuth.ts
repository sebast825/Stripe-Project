import {   useEffect, useState } from 'react';
import apiClient from '../api/client';
import { useNavigate } from 'react-router-dom';
import type { LoginResponse } from '../types/loginResponse.types';
import type { User } from '../types/user.types';
import { useUserStore } from '../states/auth/user.store';
import { useAuthStore } from '../states/auth/auth.store';



export const useAuth = () => {
  
  const [user, setUser] = useState<User | undefined>(undefined);
  const [error, setError] = useState(false);
  const navigate = useNavigate();

  const login = async (email: string, password: string) => {
  try {
      const response = await apiClient.post('/auth/login', {
        email,
        password
      });
      const data : LoginResponse = response.data;
      setUser(data.user);
      useUserStore.getState().setUser(data.user)
      useAuthStore.getState().setTokens(data.accessToken,data.refreshToken)
        navigate("/dashboard", { 
        state: { user: response.data.user } 
      });

    } catch (error) {
      setError(true);
      console.error('Login failed:', error);
    }
   }
   const logout = async ()=>{
    try {
      await apiClient.post('/auth/logout');
      navigate("/")
    } catch (error) {
      setError(true);
      console.error('Login failed:', error);
    }
   }
   
  return { user, login,error ,setError,logout};
};