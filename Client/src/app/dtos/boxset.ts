import { IProduct } from "./product.interface";
import { AlbumDtoShort } from "./album";

export class BoxSetDtoShort implements IProduct {
    id!: number;
    title!: string;
    price!: number;
}

export class BoxSetDtoExtended implements IProduct {
    id!: number;
    title!: string;
    price!: number;

    albums!: AlbumDtoShort[];
}