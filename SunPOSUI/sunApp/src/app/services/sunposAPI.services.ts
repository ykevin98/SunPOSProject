import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from, Observable, ObservedValueOf } from 'rxjs';
import * as fromModels from '../models';
import { environment } from 'src/environments/environment';

@Injectable()
export class sunposAPIService{

    categories: fromModels.ICategory[] = [];
    menuItems: fromModels.IMenu[] = [];
    restaurant: fromModels.IRestaurant= {
        RestaurantID: '',
        RestaurantLocation: '',
        RestaurantName: '',
        RestaurantPhoneNumber: '',
        RestaurantViewName: ''
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

    getMenuItems(categoryId: string){
        return this.httpClient.get<fromModels.IMenu[]>
        (this.baseURL + 'SunPOS/GetMenu' + '?restaurantName=' + this.restaurantName + '&categoryId=' + categoryId, {withCredentials: true});
    }
}
