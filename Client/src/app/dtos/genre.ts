import { AlbumDtoShort } from "./album";

export class GenreDtoShort {
    id!: number;
    name!: string;
}

export class GenreDtoExtended {
    id!: number;
    name!: string;

    albums!: AlbumDtoShort[];
}