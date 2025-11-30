export class PagedResponseDto<T> {
  data: Array<T | null> = []; // ← Equivale a IEnumerable<T?> 
  page: number = 0;
  pageSize: number = 0;
  totalItems: number = 0;
  totalPages: number = 0;
}