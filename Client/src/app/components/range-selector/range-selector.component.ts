import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'app-range-selector',
  templateUrl: './range-selector.component.html',
  styleUrls: ['./range-selector.component.scss']
})
export class RangeSelectorComponent implements OnInit, AfterViewInit {

  @Input() min!: number;
  @Input() max!: number;

  @Output() changes: EventEmitter<{ min: number, max: number }> = new EventEmitter<{ min: number, max: number }>();

  @ViewChild('container') container!: ElementRef;
  @ViewChild('minSelector') minSelector!: ElementRef;
  @ViewChild('maxSelector') maxSelector!: ElementRef;
  @ViewChild('range') range!: ElementRef;

  containerWidth!: number;

  selectedMin!: number;
  selectedMax!: number;

  get rangeLeft(): number {
    if (!this.range.nativeElement.style.left) return 0;
    return +this.range.nativeElement.style.left.replace('px', '');
  }

  set rangeLeft(value: number) {
    this.range.nativeElement.style.left = `${value}px`;
  }

  get rangeRight(): number {
    if (!this.range.nativeElement.style.right) return 0;
    return +this.range.nativeElement.style.right.replace('px', '');
  }

  set rangeRight(value: number) {
    this.range.nativeElement.style.right = `${value}px`;
  }

  constructor() { }

  ngOnInit(): void {
    this.selectedMin = this.min;
    this.selectedMax = this.max;
  }

  ngAfterViewInit(): void {
    this.containerWidth = this.container.nativeElement.clientWidth - this.maxSelector.nativeElement.clientWidth;

    this.minSelector.nativeElement.addEventListener('mousedown', this.dragStart.bind(this));
    this.maxSelector.nativeElement.addEventListener('mousedown', this.dragStart.bind(this));
  }

  private dragStart(event: any): void {
    event = event || window.event;
    event.preventDefault();

    const initialPosition = event.clientX;

    document.onmouseup = this.dragEnd.bind(this);
    document.onmousemove = (event: any) => this.drag(event.target, event, initialPosition);
  }

  private dragEnd(event: any): void {
    document.onmouseup = null;
    document.onmousemove = null;
  }

  private drag(target: any, event: any, lastPosition: number): void {
    // Calculate new position
    const newPosition = event.clientX;
    const movement = lastPosition - newPosition;

    let offset = target.offsetLeft - movement;
    if (offset < 0) offset = 0;
    if (offset > this.containerWidth) offset = this.containerWidth;

    target.style.left = `${offset}px`;
    
    // Move the colored bar which highlight the range
    this.rangeLeft = this.minSelector.nativeElement.offsetLeft;
    this.rangeRight = this.containerWidth - this.maxSelector.nativeElement.offsetLeft;
    // Calculate new range values
    this.selectedMin = Math.round(this.min + (this.rangeLeft / this.containerWidth * this.max));
    this.selectedMax = Math.round(this.max - (this.min + (this.rangeRight / this.containerWidth * this.max)));

    // Emit changes
    this.changes.emit({ min: this.selectedMin, max: this.selectedMax });

    // reset
    if (offset > 0 && offset < this.containerWidth) document.onmousemove = (event: any) => this.drag(target, event, newPosition);
  }
 
}
