import { ApiResponse } from "./apiResponse.dto";

export interface ApiResponseWithData<T> extends ApiResponse {
  data: T;
  success: boolean;
  message: string;
}
