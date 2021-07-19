import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, Type } from '@angular/core';
import { Observable } from 'rxjs';

import { Configs } from 'src/app/config/configs';
import { IProduct } from 'src/app/dtos';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient, private configs: Configs) { }

  public get<T extends IProduct>(type: Type<T>, id: number): Observable<T> {
    return this.http.get<T>(`${this.generateUrl<T>(type)}/${id}`)
  }

  public all<T extends IProduct>(type: Type<T>, { filter }: { filter?: string } = {}): Observable<T[]> {
    return this.http.get<T[]>(this.generateUrl<T>(type), { params: new HttpParams().set('filter', filter ?? '') })
  }

  private generateUrl<T>(type: Type<T>): string {
    return `${this.configs.apiUrl}${this.configs.endPoints.find(({ dto }) => dto === type)?.location}`;
  }
}
