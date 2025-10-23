import { ApiResponse } from "./apiResponse.dto";

export interface PagedResponse<T> extends ApiResponse {
  data: {
    items: T[];
    totalCount: number;
    page: number;
    pageSize: number;
  };
}
