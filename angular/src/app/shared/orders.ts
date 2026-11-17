export interface ICreateOrder {
  deliveryMethodId: number;
  basketId: string;
  shippingAddress: IShippingAddress;
}
export interface IShippingAddress {
   firstName: string;
  lastName: string;
  city: string;
  zipCode: string;
  street: string;
  state: string;
  country: string;
  postalCode: string;
}
export interface IOrder {
  id: number;
  buyerId: string;
  subTotal: number;
  total: number;
  orderDate: string;
  shippingAddress: IShippingAddress;
  orderItems: IOrderItem[];
  deliveryMethod: IDeliveryMethod;
  status: number;
}
export interface IOrderItem {
  itemOrderedId: number;
  mainImage: string;
  productName: string;
  price: number;
  quantity: number;
}

export interface IDeliveryMethod {
  id: number;
  name: string;
  price: number;
  deliveryTime: string;
  description: string;
}