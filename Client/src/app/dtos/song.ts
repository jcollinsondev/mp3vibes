import { IProduct } from "./product.interface";

export class SongDtoShort implements IProduct {
    id!: number;
    title!: string;
    price!: number;
}

export class SongDtoExtended implements IProduct{
    id!: number;
    title!: string;
    format!: string;
    duration!: number;
    price!: number;
}