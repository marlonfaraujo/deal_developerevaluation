export interface PaginationDTO {
  pageNumber: number;
  pageSize: number;
  sortBy?: string;
  sortDirection?: 'asc' | 'desc';
}

export function capitalizeFirstLetter(str: string): string {
  return str.charAt(0).toUpperCase() + str.slice(1);
}