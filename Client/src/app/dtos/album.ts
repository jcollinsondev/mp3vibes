import { IProduct } from "./product.interface";
import { SongDtoShort } from "./song";

export class AlbumDtoShort implements IProduct {
    id!: number;
    title!: string;
    price!: number;
    cover!: string;
}

export class AlbumDtoExtended implements IProduct {
    id!: number;
    title!: string;
    price!: number;
    cover!: string;

    songs!: SongDtoShort[];
}