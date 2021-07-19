import { Type } from "@angular/core";
import { 
    AlbumDtoExtended, 
    AlbumDtoShort,
    ArtistDtoExtended, 
    ArtistDtoShort, 
    BoxSetDtoExtended, 
    BoxSetDtoShort, 
    GenreDtoExtended, 
    GenreDtoShort, 
    IProduct, 
    SongDtoExtended, 
    SongDtoShort 
} from "../dtos";

export class Configs {
    apiUrl!: string;
    endPoints!: { dto: Type<any>, location: string }[];
    productTypes!: { [type: string]: Type<IProduct>[] };
}

export const APP_CONFIG: Configs = {
    apiUrl: 'https://localhost:5001/',
    endPoints: [
        { dto: GenreDtoShort, location: 'genres' },
        { dto: GenreDtoExtended, location: 'genres' },
        { dto: ArtistDtoShort, location: 'artists' },
        { dto: ArtistDtoExtended, location: 'artists' },
        { dto: BoxSetDtoShort, location: 'boxsets' },
        { dto: BoxSetDtoExtended, location: 'boxsets' },
        { dto: AlbumDtoShort, location: 'albums' },
        { dto: AlbumDtoExtended, location: 'albums' },
        { dto: SongDtoShort, location: 'songs' },
        { dto: SongDtoExtended, location: 'songs' }
    ],
    productTypes: {
        boxsets: [ BoxSetDtoShort, BoxSetDtoExtended ],
        albums: [ AlbumDtoShort, AlbumDtoExtended ],
        songs: [ SongDtoShort, SongDtoExtended ],
    }
};