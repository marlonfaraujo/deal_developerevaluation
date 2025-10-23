import { PaginationDTO } from "../../shared/pagination.dto";

export interface ProductFilterDTO extends PaginationDTO {
  name?: string;
  code?: string;
}