import {v4 as uuidv4} from 'uuid';
export interface BasketItem {
  id: string;
  name: string;
  price: number;
  quantity: number;
  category: string;
  imageUrl: string;
}
export interface Basket {
  id: string;
  items: BasketItem[];
}