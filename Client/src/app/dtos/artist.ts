import { AlbumDtoShort } from "./album";

export class ArtistDtoShort { 
    id!: number;
    name!: string;
}

export class ArtistDtoExtended { 
    id!: number;
    name!: string;

    albums!: AlbumDtoShort[];
}