import { DecimalPipe } from "@angular/common";

export interface IMenu{
    menuId: number;
    category: string;
    category2: string;
    item: string;
    item2: string;
    price: number;
    lunchPrice: number;
    dinnerPrice: number;
    description: string;
    categoryID: string;
    categoryName: string;
    restaurantID: string;
}