using haxe.root;
#pragma warning disable 109, 114, 219, 429, 168, 162
namespace alphatab.rendering.glyphs{
	public  class TabSlideLineGlyph : global::alphatab.rendering.Glyph {
		public    TabSlideLineGlyph(global::haxe.lang.EmptyObject empty) : base(global::haxe.lang.EmptyObject.EMPTY){
			unchecked {
			}
		}
		
		
		public    TabSlideLineGlyph(global::alphatab.model.SlideType type, global::alphatab.model.Note startNote, global::alphatab.rendering.glyphs.BeatContainerGlyph parent) : base(global::haxe.lang.EmptyObject.EMPTY){
			unchecked {
				global::alphatab.rendering.glyphs.TabSlideLineGlyph.__hx_ctor_alphatab_rendering_glyphs_TabSlideLineGlyph(this, type, startNote, parent);
			}
		}
		
		
		public static   void __hx_ctor_alphatab_rendering_glyphs_TabSlideLineGlyph(global::alphatab.rendering.glyphs.TabSlideLineGlyph __temp_me236, global::alphatab.model.SlideType type, global::alphatab.model.Note startNote, global::alphatab.rendering.glyphs.BeatContainerGlyph parent){
			unchecked {
				global::alphatab.rendering.Glyph.__hx_ctor_alphatab_rendering_Glyph(__temp_me236, new global::haxe.lang.Null<int>(0, true), new global::haxe.lang.Null<int>(0, true));
				__temp_me236._type = type;
				__temp_me236._startNote = startNote;
				__temp_me236._parent = parent;
			}
		}
		
		
		public static  new object __hx_createEmpty(){
			unchecked {
				return new global::alphatab.rendering.glyphs.TabSlideLineGlyph(((global::haxe.lang.EmptyObject) (global::haxe.lang.EmptyObject.EMPTY) ));
			}
		}
		
		
		public static  new object __hx_create(global::haxe.root.Array arr){
			unchecked {
				return new global::alphatab.rendering.glyphs.TabSlideLineGlyph(((global::alphatab.model.SlideType) (arr[0]) ), ((global::alphatab.model.Note) (arr[1]) ), ((global::alphatab.rendering.glyphs.BeatContainerGlyph) (arr[2]) ));
			}
		}
		
		
		public  global::alphatab.model.Note _startNote;
		
		public  global::alphatab.model.SlideType _type;
		
		public  global::alphatab.rendering.glyphs.BeatContainerGlyph _parent;
		
		public override   void doLayout(){
			unchecked {
				this.width = 0;
			}
		}
		
		
		public override   bool canScale(){
			unchecked {
				return false;
			}
		}
		
		
		public override   void paint(int cx, int cy, global::alphatab.platform.ICanvas canvas){
			unchecked {
				global::alphatab.rendering.TabBarRenderer r = ((global::alphatab.rendering.TabBarRenderer) (this.renderer) );
				int sizeX = ((int) (( 12 * this.renderer.stave.staveGroup.layout.renderer.settings.scale )) );
				int sizeY = ((int) (( 3 * this.renderer.stave.staveGroup.layout.renderer.settings.scale )) );
				int startX = default(int);
				int startY = default(int);
				int endX = default(int);
				int endY = default(int);
				{
					global::alphatab.model.SlideType _g = this._type;
					switch (global::haxe.root.Type.enumIndex(_g)){
						case 1:case 2:
						{
							int startOffsetY = default(int);
							int endOffsetY = default(int);
							if (( this._startNote.slideTarget == default(global::alphatab.model.Note) )) {
								startOffsetY = 0;
								endOffsetY = 0;
							}
							 else {
								if (( this._startNote.slideTarget.fret > this._startNote.fret )) {
									startOffsetY = sizeY;
									endOffsetY = ( sizeY * -1 );
								}
								 else {
									startOffsetY = ( sizeY * -1 );
									endOffsetY = sizeY;
								}
								
							}
							
							startX = ( cx + r.getNoteX(this._startNote, new global::haxe.lang.Null<bool>(true, true)) );
							startY = ( ( cy + r.getNoteY(this._startNote) ) + startOffsetY );
							if (( this._startNote.slideTarget != default(global::alphatab.model.Note) )) {
								endX = ( cx + r.getNoteX(this._startNote.slideTarget, new global::haxe.lang.Null<bool>(false, true)) );
								endY = ( ( cy + r.getNoteY(this._startNote.slideTarget) ) + endOffsetY );
							}
							 else {
								endX = ( ( ( cx + this._parent.x ) + this._parent.postNotes.x ) + this._parent.postNotes.width );
								endY = startY;
							}
							
							break;
						}
						
						
						case 3:
						{
							endX = ( cx + r.getNoteX(this._startNote, new global::haxe.lang.Null<bool>(false, true)) );
							endY = ( cy + r.getNoteY(this._startNote) );
							startX = ( endX - sizeX );
							startY = ( ( cy + r.getNoteY(this._startNote) ) + sizeY );
							break;
						}
						
						
						case 4:
						{
							endX = ( cx + r.getNoteX(this._startNote, new global::haxe.lang.Null<bool>(false, true)) );
							endY = ( cy + r.getNoteY(this._startNote) );
							startX = ( endX - sizeX );
							startY = ( ( cy + r.getNoteY(this._startNote) ) - sizeY );
							break;
						}
						
						
						case 5:
						{
							startX = ( cx + r.getNoteX(this._startNote, new global::haxe.lang.Null<bool>(true, true)) );
							startY = ( cy + r.getNoteY(this._startNote) );
							endX = ( startX + sizeX );
							endY = ( ( cy + r.getNoteY(this._startNote) ) - sizeY );
							break;
						}
						
						
						case 6:
						{
							startX = ( cx + r.getNoteX(this._startNote, new global::haxe.lang.Null<bool>(true, true)) );
							startY = ( cy + r.getNoteY(this._startNote) );
							endX = ( startX + sizeX );
							endY = ( ( cy + r.getNoteY(this._startNote) ) + sizeY );
							break;
						}
						
						
						default:
						{
							return ;
						}
						
					}
					
				}
				
				canvas.setColor(this.renderer.stave.staveGroup.layout.renderer.renderingResources.mainGlyphColor);
				canvas.beginPath();
				canvas.moveTo(((double) (startX) ), ((double) (startY) ));
				canvas.lineTo(((double) (endX) ), ((double) (endY) ));
				canvas.stroke();
			}
		}
		
		
		public override   object __hx_setField(string field, int hash, object @value, bool handleProperties){
			unchecked {
				switch (hash){
					case 1542788809:
					{
						this._parent = ((global::alphatab.rendering.glyphs.BeatContainerGlyph) (@value) );
						return @value;
					}
					
					
					case 1707673:
					{
						this._type = ((global::alphatab.model.SlideType) (@value) );
						return @value;
					}
					
					
					case 1570770229:
					{
						this._startNote = ((global::alphatab.model.Note) (@value) );
						return @value;
					}
					
					
					default:
					{
						return base.__hx_setField(field, hash, @value, handleProperties);
					}
					
				}
				
			}
		}
		
		
		public override   object __hx_getField(string field, int hash, bool throwErrors, bool isCheck, bool handleProperties){
			unchecked {
				switch (hash){
					case 1028568990:
					{
						return ((global::haxe.lang.Function) (new global::haxe.lang.Closure(((object) (this) ), ((string) ("paint") ), ((int) (1028568990) ))) );
					}
					
					
					case 1734479962:
					{
						return ((global::haxe.lang.Function) (new global::haxe.lang.Closure(((object) (this) ), ((string) ("canScale") ), ((int) (1734479962) ))) );
					}
					
					
					case 1825584277:
					{
						return ((global::haxe.lang.Function) (new global::haxe.lang.Closure(((object) (this) ), ((string) ("doLayout") ), ((int) (1825584277) ))) );
					}
					
					
					case 1542788809:
					{
						return this._parent;
					}
					
					
					case 1707673:
					{
						return this._type;
					}
					
					
					case 1570770229:
					{
						return this._startNote;
					}
					
					
					default:
					{
						return base.__hx_getField(field, hash, throwErrors, isCheck, handleProperties);
					}
					
				}
				
			}
		}
		
		
		public override   void __hx_getFields(global::haxe.root.Array<object> baseArr){
			unchecked {
				baseArr.push("_parent");
				baseArr.push("_type");
				baseArr.push("_startNote");
				{
					base.__hx_getFields(baseArr);
				}
				
			}
		}
		
		
	}
}


