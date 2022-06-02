import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, from, Observable, ReplaySubject } from 'rxjs';
import * as fromModels from '../models';
import { environment } from 'src/environments/environment';

@Injectable()
export class sunposAPIService{
    private baseURL = environment.apiUrl;
    private restaurantName = environment.restaurantName;

    private shoppingCart$: BehaviorSubject<fromModels.ICart>;

    constructor(private httpClient: HttpClient){
        this.shoppingCart$ = new BehaviorSubject<fromModels.ICart>(
            {
                menuId: 0,
                item: '',
                item2: '',
                price: 0,
                lunchPrice: 0,
                dinnerPrice: 0,
                description: '',
                userId: '',
                restaurantId: '',
                itemId: ''
            }
        )
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

    addToCart(menuItem: fromModels.IMenu, userId: string, restaurantId: string): Observable<fromModels.IResult>{
        const headers = { 'content-type': 'application/json'};
        const body = JSON.stringify(menuItem);
        return this.httpClient.post<fromModels.IResult>
        (this.baseURL + 'SunPOS/AddToCart' + '?userId=' + userId + '&restaurantId=' + restaurantId, body, {'headers': headers, withCredentials: true });
    }

    checkout(cartItems: fromModels.ICart[]): Observable<fromModels.IResult>{
        const headers = { 'content-type': 'application/json'};
        const body = JSON.stringify(cartItems);
        return this.httpClient.delete<fromModels.IResult>
        (this.baseURL + 'SunPOS/Checkout', {'headers': headers, 'body': body, withCredentials: true});
    }

    removeCartItem(itemId: string): Observable<fromModels.IResult>{
        const headers = { 'content-type': 'application/json'};
        return this.httpClient.delete<fromModels.IResult>
        (this.baseURL + 'SunPOS/RemoveCartItem' + '?itemId=' + itemId, {withCredentials: true });
    }

    private getShoppingCart(userId: string, restaurantId: string){
        this.httpClient.get<fromModels.ICart>
        (this.baseURL + 'SunPOS/GetShoppingCart' + '?userId=' + userId + '&restaurantId=' + restaurantId, { withCredentials: true})
        .subscribe((result) =>{
            this.setShoppingCart(result);
        });
    }

    private setShoppingCart(shoppingCart: fromModels.ICart){
        this.shoppingCart$.next(shoppingCart);
    }
}
