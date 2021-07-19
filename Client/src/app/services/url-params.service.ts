import { Injectable } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UrlParamsService {

  constructor() { }

  public subscribe<TParams>(route: ActivatedRoute, next: (params: TParams) => void): Subscription {
    // Response Function
    const responseHandler = (): void => next({ ...route.snapshot.params, ...route.snapshot.queryParams } as TParams);

    const queryParamsSubscription = route.queryParams.subscribe(responseHandler);
    const paramsSubscription = route.params.subscribe(responseHandler);

    return queryParamsSubscription.add(paramsSubscription);
  }
}
