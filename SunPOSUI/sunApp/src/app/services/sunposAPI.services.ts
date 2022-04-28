import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from, Observable, ObservedValueOf } from 'rxjs';
import * as fromModels from '../models';
import { environment } from 'src/environments/environment';

@Injectable()
export class sunposAPIService{

    categories: fromModels.ICategory[] = [];
    menuItems: fromModels.IMenu[] = [];
    cartItems: fromModels.ICart[] = [];
    restaurant: fromModels.IRestaurant= {
        restaurantID: '',
        restaurantLocation: '',
        restaurantName: '',
        restaurantPhoneNumber: '',
        restaurantViewName: '',
        mondayHours: '',
        tuesdayHours: '',
        wednesdayHours: '',
        thursdayHours: '',
        fridayHours: '',
        saturdayHours: '',
        sundayHours: ''
    };

    private baseURL = environment.apiUrl;
    private restaurantName = environment.restaurantName;

    constructor(private httpClient: HttpClient){

    }

    getRestaurant(): Observable<fromModels.IRestaurant>{
        return this.httpClient.get<fromModels.IRestaurant>
        (this.baseURL + 'SunPOS/GetRestaurant' + '?restaurantName=' + this.restaurantName, {withCredentials: true});
    }

    getCategories(restaurantId: string): Observable<fromModels.ICategory[]>{
        return this.httpClient.get<fromModels.ICategory[]>
        (this.baseURL + 'SunPOS/GetCategories' + '?restaurantId=' + restaurantId, {withCredentials: true});
    }

    getMenuItems(categoryId: string): Observable<fromModels.IMenu[]>{
        return this.httpClient.get<fromModels.IMenu[]>
        (this.baseURL + 'SunPOS/GetMenu' + '?restaurantName=' + this.restaurantName + '&categoryId=' + categoryId, {withCredentials: true});
    }

    getUser(userName: string): Observable<fromModels.IUser>{
        return this.httpClient.get<fromModels.IUser>
        (this.baseURL + 'SunPOS/GetUser' + '?userName=' + userName, {withCredentials: true });
    }

    getCartItems(userId: string, restaurantId: string): Observable<fromModels.ICart[]>{
        return this.httpClient.get<fromModels.ICart[]>
        (this.baseURL + 'SunPOS/GetShoppingCart' + '?userId=' + userId + '&restaurantId=' + restaurantId, { withCredentials: true});
    }

    addUser(user: fromModels.IUser): Observable<fromModels.IResult>{
        const headers = { 'content-type': 'application/json'}  
        const body = JSON.stringify(user);
        return this.httpClient.post<fromModels.IResult>(this.baseURL + 'SunPOS/AddUser', body, {'headers': headers, withCredentials: true });
    }

    addToCart(menuItem: fromModels.IMenu): Observable<fromModels.IResult>{
        const headers = { 'content-type': 'application/json'};
        const body = JSON.stringify(menuItem);
        return this.httpClient.post<fromModels.IResult>(this.baseURL + 'SunPOS/AddToCart', body, {'headers': headers, withCredentials: true });
    }
}
